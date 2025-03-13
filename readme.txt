docker run --name salesdb-container -e POSTGRES_USER=salesuser -e POSTGRES_PASSWORD=salespassword -e POSTGRES_DB=salesdb -p 5432:5432 -d postgres:latest
