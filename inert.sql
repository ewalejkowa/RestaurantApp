INSERT INTO PRACOWNIK VALUES ('Ewa','Lejk');
INSERT INTO PRACOWNIK VALUES ('Marta','Lasota');
INSERT INTO PRACOWNIK VALUES ('Agnieszka','Dargacz');
INSERT INTO PRACOWNIK VALUES ('Anna','Nowak');
INSERT INTO PRACOWNIK VALUES ('Tomasz','Wojciechowski');
INSERT INTO PRACOWNIK VALUES ('Adam','Wo�niak');

INSERT INTO MENU VALUES ( 'MENU');
INSERT INTO Kategoria VALUES ( 'Deser');
INSERT INTO Kategoria VALUES ( 'Napoje');
INSERT INTO Kategoria VALUES ( 'Danie g��wne');

INSERT INTO POZYCJA_MENU VALUES ( 3.5, 'Woda niegazowana', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 3.5, 'Woda gazowana', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Sok pomara�czowy', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Sok jab�kowy', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Herbata', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Kawa', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 10, 'Szarlotka', 1,1);
INSERT INTO POZYCJA_MENU VALUES ( 10 ,'Sernik', 1,1);
INSERT INTO POZYCJA_MENU VALUES ( 12, 'Lodowe marzenie', 1,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Sok jab�kowy', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Herbata', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 4, 'Kawa', 2,1);

INSERT INTO POZYCJA_MENU VALUES ( 15, 'Spaghetti', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 19, 'Pizza Margharitta', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 21, 'Pizza Hawajska', 2,1);
INSERT INTO POZYCJA_MENU VALUES ( 23, 'Pizza Pepperoni', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 24, 'Pizza Wiejska', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 26, 'Pizza Gyros', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 12, 'Sa�atka Wiosenna', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 25, 'Kotlet schabowy z ziemniakami i sur�wk�', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 30, 'De volaille z ziemniakami i sur�wk�', 3,1);


INSERT INTO POZYCJA_MENU VALUES (30, 'Pier� z kurczaka z ziemniakami i sur�wk�', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 35, 'Kark�wka z grilla z frytkami i sur�wk�', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 6, 'Ros�', 3,1);
INSERT INTO POZYCJA_MENU VALUES ( 7, 'Zupa Pomidorowa', 3,1);

insert into STATUS values ('nowy')
insert into STATUS values ('nowy2')
insert into STATUS values ('nowy3')
INSERT INTO ZAMOWIENIE VALUES (24,'2017-11-14 12:00', '2017-11-14 12:15' ,'2017-11-14 12:17', 'uwagi ',1,1,1,1)

INSERT INTO ZAMOWIENIE VALUES (30,'2017-11-14 12:15','2017-11-14 12:20','2017-11-14 12:25','uwagi ',1,1,2,2)

INSERT INTO ZAMOWIENIE VALUES (23,'2017-11-14 12:30','2017-11-14 12:35','2017-11-14 12:40','uwagi ',1,1,1,1)

INSERT INTO ZAMOWIENIE VALUES (15,'2017-11-14 12:50','2017-11-14 12:55','2017-11-14 12:57','uwagi ',1,1,3,3)

INSERT INTO REALIZACJA_ZAMOWIENIA VALUES (1,6)
INSERT INTO REALIZACJA_ZAMOWIENIA VALUES (2,7)
INSERT INTO REALIZACJA_ZAMOWIENIA VALUES (1,8)
INSERT INTO REALIZACJA_ZAMOWIENIA VALUES (3,9)

INSERT INTO ELEMENT_ZAMOWIENIA VALUES ( 1,7,18)
INSERT INTO ELEMENT_ZAMOWIENIA VALUES ( 1,7,22)
INSERT INTO ELEMENT_ZAMOWIENIA VALUES ( 1,8,17)
INSERT INTO ELEMENT_ZAMOWIENIA VALUES ( 1,9,19)
