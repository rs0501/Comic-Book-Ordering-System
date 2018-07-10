echo off

rem batch file to run a script to create a db

REM sqlcmd -S LAPTOP-61UA4Q4T\SQLEXPRESS -E -i comicOrderingDB.sql
sqlcmd -S localhost -E -i comicOrderingDB.sql

ECHO if no error messages appear DB was created
PAUSE