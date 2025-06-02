# Loja do Seu Manoel - API de Empacotamento

API REST em ASP.NET Core 8, desenvolvida para automatizar o processo de empacotamento de pedidos da loja do Seu Manoel.

Dado um pedido com uma lista de produtos e suas dimensões, a aplicação calcula automaticamente quais caixas devem ser utilizadas, otimizando o uso do espaço.

---

##  Tecnologias Utilizadas

- ASP.NET Core 8
- Entity Framework Core 9
- SQL Server 2022
- JWT (autenticação)
- BCrypt (criptografia de senha)
- xUnit (testes unitários)
- Docker + Docker Compose
- Swagger/OpenAPI

---

## Pré-requisitos

- Docker
- Docker Compose

---

## Como executar a aplicação

### 1. Clone o repositório

```bash
git clone https://github.com/seu-usuario/LojaDoSeuManoel.git
cd LojaDoSeuManoel
```

### 2. Rode os containers com Docker Compose

```bash
docker-compose up --build
```

> Isso irá subir a API e o banco de dados. As migrations são aplicadas automaticamente no startup.

### 3. Acesse a documentação Swagger

```
http://localhost:5000/swagger
```

---

## Autenticação (JWT)

Para acessar os endpoints protegidos, siga os passos:

1. **Registre um novo usuário:**
   ```
   POST /api/Auth/register
   ```

2. **Realize o login para obter o token:**
   ```
   POST /api/Auth/login
   ```

3. **Copie o token JWT** e clique no botão **"Authorize"** no Swagger, usando o formato:
   ```
   Bearer seu_token_aqui
   ```

---

## Endpoints principais

| Método | Rota                       | Descrição                              |
|--------|----------------------------|----------------------------------------|
| POST   | `/api/Auth/register`       | Cria novo usuário                      |
| POST   | `/api/Auth/login`          | Gera token de autenticação             |
| POST   | `/api/Pedidos/empacotar`   | Retorna quais caixas serão usadas      |

---

## Testes Unitários

O projeto inclui uma suite de testes unitários desenvolvida com **xUnit**.

### Como executar os testes (fora do container):

```bash
dotnet test
```

> Os testes verificam a lógica de empacotamento (alocação correta de produtos em caixas).

---

## Containers gerados

- `api`: Projeto ASP.NET Core + Swagger
- `sqlserver`: Banco de dados SQL Server 2022

---

## Diferenciais implementados

- ✅ Autenticação via JWT
- ✅ Criptografia de senha com BCrypt
- ✅ Migrations aplicadas automaticamente no startup
- ✅ Lógica real de empacotamento por volume (múltiplas caixas)
- ✅ Swagger com suporte para autenticação JWT
- ✅ Testes unitários cobrindo a lógica de empacotamento
- ✅ Docker Compose com ambiente pronto para testes

---

## Observações finais

- Projeto segue boas práticas de Clean Code e separação de responsabilidades
- Ideal para avaliações técnicas e demonstrações em ambientes controlados
- Nenhuma configuração adicional necessária após `docker-compose up --build`
