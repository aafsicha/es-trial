# Playground with EventStoreDb

To start the db :
>docker run --name esdb-node -it -p 2113:2113 -p 1113:1113  eventstore/eventstore:23.6.0-alpha-arm64v8 --insecure --run-projections=All --enable-atom-pub-over-http

Then you can launch the .sln and you will reach the Swagger.
Try posting and getting, you'll get there eventually.