create table Contatos
(
    Id       uniqueidentifier        not null
        constraint PK_Contatos
            primary key,
    Nome     varchar(300)            not null,
    Ddd      nvarchar(max),
    Numero   nvarchar(max),
    Email    varchar(200) default '' not null,
    Status   tinyint                     not null,
);

