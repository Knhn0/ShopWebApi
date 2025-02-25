SET search_path TO public;
DROP EXTENSION IF EXISTS "uuid-ossp";

CREATE EXTENSION "uuid-ossp" SCHEMA public;

CREATE TABLE IF NOT EXISTS public."Products"
(
    "Id" uuid  DEFAULT public.uuid_generate_v4() NOT NULL,
    "Description" character varying(255) NOT NULL,
    "Name" character varying(50) NOT NULL,
    "Price" decimal NOT NULL,
    "Image" character varying(255) NOT NULL,
    PRIMARY KEY ("Id")
    );