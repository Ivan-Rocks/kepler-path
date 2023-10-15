create table users(
    user_id serial primary key,
    first_name varchar(255) not null,
    last_name varchar(255) not null,
    email_address varchar(255) null,
    password varchar(255) not null
);