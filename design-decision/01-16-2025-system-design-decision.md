# 01/16/2025 System Design Decision

## Initial Problem Statement

The user wants to maintain Namecheap DDNS record. The reason that the user don't want to use other DDNS client is because it lacks of GUI. Namecheap does offer a client for Windows machine, but user is using LXC (Linux Container) for hosting the DDNS client with Docker installed inside the container.

## Problem Breakdown

Since this project have a requirement for GUI and automatic DDNS update functionality, we will need to implement a full stack solution for this case. We will need to see the requirements for frontend, backend, and database components.

### Front end questions

1. How many users are going to use this system?
    - Only 1 user
    - It is most likely that we don't need to put the load balancer inside our equation.
2. Will this DDNS client get host in public or private network?
    - Private network
    - Now we got confirmed that there will be only 1 user that stays inside private network. So it is not necessary for creating a login page. If the user wants to grant a basic auth, they can use reverse proxy for that. Let's keep it simple for this project.
3. How many pages the user will be able to access?
    - Only 2 pages. One for maintain the record. One for dashboard that shows the latest update status.
    - So we can build only 2 pages for answering each purpose.
4. Which thing do you expect to see inside the dashboard?
    - Well, I just want to see when is the latest success update.

### Back end questions

1. By meaning of maintaining records, do you mean you wish to create, read, update, and delete record?
    - Yes. I should be able to create, read, update, and delete the record.
    - So it is the basic CRUD opration for maintaining the record. We will need to ask for data record question on database section.
2. How frequent are you going to maintain the record?
    - Probably once in awhile. Maybe let's say 2 times per month.
    - Having rate limit also not make sense for this low amount of usage. We also have only 1 single user so limiting usage for 1 user for the whole system might also not effective.
3. How frequent you want to let the DDNS update the record?
    - Whenever the IP is change.
    - Since there is no event trigger for IP change in our system, we will need to create some kind of cron job to trigger the public IP check every 1 minute.
4. How frequent are you going to see the dashboard?
    - Also once in awhile. Maybe 5 times per month.
    - We still don't need to create rate limit for this case.

### Database questions

1. Do all of the records will always use the same public IP?
    - Yes please. All of the record will use the same public IP.
    - So we can just keep the previous IP and current IP inside the database. The record size of ip will be around 15 characters which cost `15 bytes`.
2. How many domain record you are wishing to maintain?
    - Maybe let's say around 20 records of domains. It will contains domain record, and password. Here's the sample data that we will be using.
    ```
    {
        "domain":"sample.mydomain.com",
        "password":"encryptedPasswordKeptInBase64"
    }
    ```
    - After consideration, the domain record might have around `20 bytes` per record. But for the password, since it is possible that the user will have more than 1 password, it might be better for us to create the credential database and reuse the same password for saving space for duplicate record. The size of password is still unknown for now because we don't know which cryptography method we are going to use. So we will need to decide it now.
4. Do you have any prefer cryptography method for keeping the password to be safe?
    - You can decide by your own. Choose any that makes sense.
    - The password length will have 32 characters or `32 bytes` for Namecheap. Let's use AES for keeping the secret. We will need to keep the key and iv for this case. We can use AES with base64 format for 256 bits key length. After making the key in base64, it will cost `44 bytes` for keeping the key. The IV that we will be using is `16 bytes`. So in total, it would cost around `60 bytes` per record. In common practice, we usually keep the secret key inside secure key store. But for this case, let's keep the encryption key as the file. So in case of bad actor got an access to database, they still don't have an access to the cryptography key.

Now let's do some math calculation for database estimation.

1. IP record will cost around 15 bytes.
2. Domain name record will takes around 20 bytes.
3. Encrypted password record would take around 64 bytes.
4. 20 domains with same password record would take around (20 + 64) * 20 = 1,680 bytes.
5. Previous and current IP record would cost around 30 bytes.


### Non functional requirement

Based on personal experience, there's used to be the case that some IP update operation will fail. So maybe it is better for us to keep the current IP record with the domain record. Which it will increase from 84 bytes record to around 100 bytes record. The JSON structure will look something like this.

```
{
    "domain":"sample.mydomain.com",
    "password":"encryptedPasswordKeptInBase64",
    "ip":"123.123.123.123"
}
```

## System Design