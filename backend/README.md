# Namecheap DDNS Updater Web UI

In this project, we will be creating 3 backend. Each backend will act as the standalone microservice. The reason for using microservice for this case is to make sure that one functionality break does not cause the whole application to be break. In case you are wondering what is the functionality for each of backend, here are some explanation for each of backend component.

## Record Service

This service is for create, read, update, and delete domain record. This service will be the front facing API which will have `/record` endpoint. The front end will call `/record` endpoint for performing any task that the front end wants.

## Password Service

This service is for create, read, update, and delete the password. This service will have `/password` endpoint for performing every operation related to the password. This service will get called by both record and DDNS IP service.

## DDNS IP Service

This service is for update the record. This is the only cron job with zero endpoint. It is the worker that will check the IP whether the current public IP is already changing or not. If the IP is already changing, then it will apply a new IP to the rest of record. This IP operation will get triggered every 1 minute.