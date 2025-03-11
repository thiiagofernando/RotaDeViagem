
# Rota de Viagem

CRUD de cadastro de ROTAS


## Stack utilizada

**Front-end:** Angular 17

**Back-end:** C#, .NET 8


## Instalação

Clonando o projeto com o comando git clone

```bash
  git clone https://github.com/thiiagofernando/RotaDeViagem.git
```


Executando o projeto de backend da API, acesse a pasta RotaDeViagem e execute os comandos

```bash
  cd RotaDeViagemApi
  dotnet run
```

Abra o navegador e acessa a url, para ver a documentação no swagger

```bash
  http://localhost:5272/swagger
```


Executando o projeto de backend de teste, acesse a pasta RotaDeViagem e execute os comandos

```bash
  cd RotaDeViagemTest
  dotnet test '--logger:console;verbosity=normal' --verbosity minimal --collect:"XPlat Code Coverage"
```


Instalando o Angular CLI, caso não tenha ele instalado, abra o terminal e execute o comando

```bash
  npm install -g @angular/cli@17.2.2
```

Instalando as dependência do projeto de frontend, acesse a pasta RotaDeViagem e execute os comandos

```bash
  cd rota-viagem-app
  npm install --legacy-peer-deps
```


Configurando variável de ambiente do projeto de frontend, acesse o caminho RotaDeViagem\rota-viagem-app\src\environments e edite o arquivo environment.ts

```bash
  altere a propriedade apiUrl informado a url e porta da API que esta executando localmente
  apiUrl: 'https://localhost:7012'
```


Executando o projeto de frontend, acesse a pasta RotaDeViagem e execute os comandos

```bash
  cd rota-viagem-app
  ng serve
```

Abra o navegador e acessa a url

```bash
  http://localhost:4200
```