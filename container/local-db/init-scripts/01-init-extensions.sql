CREATE EXTENSION IF NOT EXISTS pg_uuidv7;

-- spatial data extension
CREATE EXTENSION IF NOT EXISTS postgis;

-- Local DB for integration tests.
-- This is a separate database from the main playground database, and can frequently be dropped and recreated.
CREATE DATABASE playground_test WITH OWNER playground;
GRANT ALL PRIVILEGES ON DATABASE playground_test TO playground;
ALTER DATABASE playground_test SET search_path TO public;