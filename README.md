# FootballClub

Ссылка на демо - https://youtu.be/5oy4lomcYYo

1) Установите MySql Server и импортируйте дамп базы данных, который находится по ссылке - https://disk.yandex.ru/d/dSDxe1t36qyzng;
3) В appsettings.json укажите строку подключения к базе данных FootballClub, а также к information_schema;
2) Соберите проект в Visual Studio.

Первый пункт выполнять необязательно, так как взаимодействие с базой данных основано на EF Core, который создаст базу данных, если он её не найдёт. 
Но тогда в таком случае не будет данных в базе. В дампе из первого пункта есть тестовые данные.
