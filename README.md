# Autoware.RecallSystem test

Para rodar esse projeto clone o repositório e siga os passos a seguir:

Primeiro, crie o banco e suas tabelas seguindo os passos abaixo.

Eu prefiro rodar servidores de bancos de dados em instâncias do docker,
mas se já tiver um banco rodando é só pular para a parte das queries no passo 3.

(Estou usando o WSL2 então alguns comandos podem ser diferentes no Windows).

## 1. Criar instância de banco no docker
O primeiro passo é rodar esse comando no seu terminal, e ele irá baixar e subir 
a versão mais recente do sql server

``` 
docker run -v ~/docker --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server

```

Se não tiver acontecido nenhum erro agora existe uma instância do banco na porta 1433

## 2. No seu cliente (estou usando Azure Data Studio)
Para conectar ao banco é necessário escolher o tipo de autenticação certa:
1. Escolha SQL Login
2. username: sa
3. password: 1q2w3e4r@#$


## 3. Criar o banco no SQL Server

```
CREATE DATABASE RecallsDB;
```

## 4. Criar as tabelas 

```
USE RecallsDB;

CREATE TABLE Recalls (
    Id INT PRIMARY KEY,
    Titulo NVARCHAR(255) NOT NULL,
    Descricao NVARCHAR(MAX) NOT NULL,
    DataPublicacao DATETIME2 NOT NULL
);

CREATE TABLE ExecucoesRecalls (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RecallId INT NOT NULL,
    Chassi NVARCHAR(18) NOT NULL,
    DataExecucao DATETIME2 NULL,
    Concessionaria NVARCHAR(255) NULL,
    FOREIGN KEY (RecallId) REFERENCES Recalls(Id)
);
```

## 5. Inserir os dados

```
INSERT INTO Recalls (Id, Titulo, Descricao, DataPublicacao) VALUES
(1, 'TROCA DISCO FREIO', 'Poderá resultar em ruídos anormais e deficiência na frenagem.', '2023-10-29'),
(2, 'ATUALIZAÇÃO DE SOFTWARE', 'Poderá resultar em funcionamento incorreto do monitoramento dos sensores.', '2019-04-01'),
(3, 'INSPEÇÃO SISTEMA DE PARTIDA', 'Poderá resultar em funcionamento incorreto na partida do veículo.', '2021-01-30'),
(4, 'CORROSÃO CABO DO ACELERADOR', 'Poderá resultar em quebra no cabo, impedindo a acelaração do veículo.', '2020-03-15'),
(5, 'SUBSTITUIÇÃO SENSOR DO VELOCÍMETRO', 'Poderá resultar em registro incorreto da quilometragem.', '2014-07-10');

INSERT INTO ExecucoesRecalls (RecallId, Chassi, DataExecucao, Concessionaria) VALUES
(4, 'CHASSI123456789012', '2024-05-01', 'Horizon Motors'),
(2, 'CHASSI234567890123', '2020-03-25', 'Velocity Auto Group'),
(5, 'CHASSI234567890123', '2016-10-07', 'Velocity Auto Group');
```

Pronto agora o banco está com as tabelas e os dados de exemplo dos arquivos JSON.

Agora é necessário buildar a solução

