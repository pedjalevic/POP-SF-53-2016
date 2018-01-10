CREATE DATABASE pop-sf53;

USE pop-sf53;

CREATE TABLE TipNamestaja(
Id INT IDENTITY(1,1) PRIMARY KEY,
Naziv VARCHAR(20),
Obrisan BIT DEFAULT 0
);

CREATE TABLE Namestaj(
Id INT  IDENTITY (1, 1) PRIMARY KEY,
Naziv VARCHAR (20) ,
Kolicina INT,
Sifra VARCHAR (15) ,
Tip_Namestaja INT,
Cena DECIMAL,
AkcijskaCena DECIMAL ,
Obrisan BIT DEFAULT 0,
);

CREATE TABLE DodatneUsluge(
Id INT IDENTITY (1, 1) PRIMARY KEY,
Naziv  VARCHAR (15),
Cena DECIMAL,
Obrisan BIT DEFAULT 0,
);

CREATE TABLE Korisnik(
Id INT IDENTITY (1, 1) PRIMARY KEY,
Ime VARCHAR (20),
Prezime VARCHAR (20),
Korisnicko_Ime VARCHAR (20),
Lozinka VARCHAR (20),
Tip_Korisnika VARCHAR (20)  CHECK (Tip_Korisnika='Prodavac' OR Tip_Korisnika='Administrator'),
Obrisan BIT DEFAULT 0,
);

CREATE TABLE Akcija(
Id INT IDENTITY (1, 1) PRIMARY KEY,
Datum_Pocetka DATETIME,
Datum_Kraja DATETIME,
Popust INT,
Obrisan BIT  DEFAULT 0,
);

CREATE TABLE NaAkciji(
Id INT IDENTITY (1, 1) PRIMARY KEY,
NamestajId INT REFERENCES Namestaj(Id),
AkcijaId INT REFERENCES Akcija(Id),
);

CREATE TABLE Prodaja(
Id INT IDENTITY (1, 1) PRIMARY KEY,
Kupac VARCHAR (30),
Broj_Racuna INT,
Datum_Prodaje DATETIME,
Ukupan_Iznos DECIMAL,
Obrisan BIT DEFAULT 0,
);

CREATE TABLE Stavka(
Id INT IDENTITY (1, 1) PRIMARY KEY,
Kolicina INT,
Cena DECIMAL,
NamestajId INT REFERENCES Namestaj(Id),
ProdajaId INT REFERENCES Prodaja(Id),
);

CREATE TABLE ProdateUsluge(
Id INT IDENTITY (1, 1) PRIMARY KEY,
UslugeId INT REFERENCES DodatneUsluge(Id),
ProdajaId INT REFERENCES Prodaja(Id),
);

CREATE TABLE Salon(
Id INT IDENTITY(1,1) PRIMARY KEY,
Naziv VARCHAR(50),
Adresa VARCHAR(50),
Broj_telefona VARCHAR(20),
Email VARCHAR(20),
Adresa_sajta VARCHAR(20),
PIB VARCHAR(20),
Maticni_broj INT ,
Broj_ziro_racuna VARCHAR(20)
);

