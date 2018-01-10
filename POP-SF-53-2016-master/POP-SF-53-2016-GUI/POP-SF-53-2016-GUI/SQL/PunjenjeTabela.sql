INSERT INTO TipNamestaja(Naziv,Obrisan) 
VALUES('Krevet',0);
INSERT INTO TipNamestaja(Naziv,Obrisan) 
VALUES('Stolica',0);

INSERT INTO Namestaj(Naziv,Kolicina,Sifra,Tip_Namestaja,Cena,AkcijskaCena,Obrisan)
VALUES ('Krevet Mira',500,'KV1',1,25000,0,0);
INSERT INTO Namestaj(Naziv,Kolicina,Sifra,Tip_Namestaja,Cena,AkcijskaCena,Obrisan)
VALUES ('Hoklica',75,'ST1',2,800,0,0);
INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan)
VALUES('Dostava',4000,0);
INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan)
VALUES('Montaza',3000,0);

INSERT INTO Korisnik(Ime,Prezime,Korisnicko_Ime,Lozinka,Tip_Korisnika,Obrisan)
VALUES('Mika','Mikic','levic','123','Administrator',0);
INSERT INTO Korisnik(Ime,Prezime,Korisnicko_Ime,Lozinka,Tip_Korisnika,Obrisan)
VALUES('Petar','Peric','levic1','123','Prodavac',0);

INSERT INTO Salon (Naziv,Adresa,Broj_telefona,Email,Adresa_sajta,PIB,Maticni_broj,Broj_ziro_racuna) 
VALUES('Levic pro','BB,Levici','037-736-007','levicpro@gmail.com','www.levicpro.com','16789452',2570,'750-16-1-2784');