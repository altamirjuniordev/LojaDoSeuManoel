# Loja do Seu Manoel - API de Empacotamento

API desenvolvida para automatizar o processo de empacotamento de pedidos da loja do Seu Manoel.

A aplicação recebe uma lista de produtos com dimensões e retorna quais caixas devem ser utilizadas, otimizando o uso do espaço.

---

## ? Pré-requisitos

- Docker
- Docker Compose

---

## ?? Como rodar a aplicação

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/LojaDoSeuManoel.git
cd LojaDoSeuManoel
```

2. Rode com Docker Compose:
```bash
docker-compose up --build
```

3. Acesse o Swagger em:
```
http://localhost:5000/swagger
```

---

## ?? Autenticação

Para acessar os endpoints protegidos (como `/api/Pedidos/empacotar`), você precisa:

1. Criar um usuário via:
```
POST /api/Auth/register
```

2. Fazer login com:
```
POST /api/Auth/login
```

3. Copiar o token JWT retornado e clicar em **"Authorize"** no Swagger, colando assim:
```
Bearer seu_token_aqui
```

---

## ?? Endpoints principais

- `POST /api/Auth/register` ? Cria usuário
- `POST /api/Auth/login` ? Gera token
- `POST /api/Pedidos/empacotar` ? Retorna caixas e produtos organizados

---

## ?? Containers gerados

- **API**: ASP.NET Core 8
- **Banco de dados**: SQL Server 2022

---

## ?? Diferenciais implementados

- ? Autenticação com JWT e senha com BCrypt
- ? Endpoints protegidos com `[Authorize]`
- ? Swagger configurado para uso com token
- ? Lógica real de empacotamento com múltiplas caixas
- ? Entidades bem modeladas e banco com dados `Seed`
- ? Docker Compose pronto para uso imediato

---

## ?? Observações finais

- Projeto segue princípios de Clean Architecture
- Ideal para ambientes de teste e demonstração técnica