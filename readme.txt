Miałem problem z połączeniem sie z bazą danych na szkolnym serwerze więć postawiłem lokalnie swoją, mam nadzieje że nie bedzie z tym problemu :).

Skrypcik do stworzenia tabeli:

CREATE TABLE Animal
(
    IdAnimal INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200),
    Description NVARCHAR(200) NULL,
    Category NVARCHAR(200),
    Area NVARCHAR(200)
);