# Record Service

Record service is for performing a record operation. It consists of create record, read record, update record, and delete record. The following are the API spec for each endpoints.

## Endpoints

### POST /record

Body
```
{
  "domain": "string",
  "password": "string"
}
```

### GET /record

Response body
```
[
  {
    "domain": "string",
    "password": "string",
    "ip": "string"
  }
]
```

### GET /record/{domainName}

Response body
```
{
  "domain": "string",
  "password": "string",
  "ip": "string"
}
```

### PUT /record

Body
```
{
  "domain": "string",
  "password": "string"
}
```

### DELETE /record/{domainName}