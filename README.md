# Loja do Seu Manoel - API de Empacotamento

API desenvolvida para automatizar o processo de empacotamento de pedidos da loja do Seu Manoel.

A aplica��o recebe uma lista de produtos com dimens�es e retorna quais caixas devem ser utilizadas, otimizando o uso do espa�o.

---

## ? Pr�-requisitos

- Docker
- Docker Compose

---

## ?? Como rodar a aplica��o

1. Clone o reposit�rio:
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

## ?? Autentica��o

Para acessar os endpoints protegidos (como `/api/Pedidos/empacotar`), voc� precisa:

1. Criar um usu�rio via:
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

- `POST /api/Auth/register` ? Cria usu�rio
- `POST /api/Auth/login` ? Gera token
- `POST /api/Pedidos/empacotar` ? Retorna caixas e produtos organizados

---

## ?? Containers gerados

- **API**: ASP.NET Core 8
- **Banco de dados**: SQL Server 2022

---

## ?? Diferenciais implementados

- ? Autentica��o com JWT e senha com BCrypt
- ? Endpoints protegidos com `[Authorize]`
- ? Swagger configurado para uso com token
- ? L�gica real de empacotamento com m�ltiplas caixas
- ? Entidades bem modeladas e banco com dados `Seed`
- ? Docker Compose pronto para uso imediato

---

## ?? Observa��es finais

- Projeto segue princ�pios de Clean Architecture
- Ideal para ambientes de teste e demonstra��o t�cnica