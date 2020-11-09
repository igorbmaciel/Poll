# Poll

Projeto Realizado para o desafio da Alterdata

<b>Qual arquitetura foi escolhida?</b><br/>
R - Foi utilizado o .net core 2.2 com o conceito de DDD e utilizando command e handler, além de estar rodando no docker.

<b>Porque foi escolhido essa arquitetura?</b><br/>
R- Foi escolhida pois o .net core é um cross plataform, sendo facilmente colocado junto ao docker em qualquer ambiente, além do conceito de DDD utilizando command e handler, separando cada responsabilidade, dando facil entendimento e manutenção do código, bem como seu ciclo de vida facilmente utilizável.

<b>Quais endpoints foram desenvolvidos?</b><br/>
R- Foram criados três endpoints para a finalidade de criar funcionários, tarefas e a votação em si.

<b>Explicando cada endpoint</b>

O Primeiro endpoint é o de <b>Employee</b>, com os seguintes métodos:<br/>

POST<br/>
/api/Employee <br/>
<b>Esperando receber como parâmetros:</b><br/>
{
  "Name": "string",
  "Email": "string",
  "Password": "string"
}
<br/><br/>
<b>Irá retornar o funcionário cadastrado:</b>
<br/>
{
  "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Name": "string",
  "Email": "string"
}
<br/><br/>
GET<br/>
/api/Employee<br/>
<b>Irá retornar uma lista de funcionários:</b><br/>
[
  {
    "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "Name": "string",
    "Email": "string"
  }
]
<br/><br/>

O Segundo endpoint é o de <b>Task</b>, com os seguintes métodos:<br/><br/>

POST
/api/Task <br />
<b>Esperando receber como parâmetros:</b><br/>
{
  "Name": "string"
}<br/>
<b>Irá retornar a tarefa cadastrada:</b>
<br/>
{
  "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Name": "string"
}
<br/><br/>

GET
/api/Task<br/>
<b>Irá retornar uma lista de tarefas:</b><br/>
[
  {
    "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "Name": "string"
  }
]
<br/><br/>

GET
/api/Task/EmployeeVotes <br/>
<b>Irá retornar uma lista de tarefas com os usuários e a hora em que eles votaram:</b><br/>
[
  {
    "TaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "TaskName": "string",
    "EmployeeVotes": [
      {
        "TasksId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "EmployeeName": "string",
        "Date": "2020-11-09T01:36:29.537Z"
      }
    ]
  }
]
<br/><br/>

GET
/api/Task/Votes <br/>
<b>Irá retornar uma listagem das tarefas com o número de votos. Esta listagem está na ordem das tarefas mais votadas:</b><br/>
[
  {
    "TaskName": "string",
    "QuantityVotes": 0
  }
]
<br/><br/>

O Terceiro endpoint é o de <b>Vote</b>, com o seguinte método:<br/><br/>

POST
/api/Vote <br/>
Este método irá armazenar os votos de cada funcionário em uma tarefa.<br/>
<b>Esperando receber como parâmetros:</b><br/>
{
  "EmployeeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "TaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Comment": "string"
}
<br/><br/>
<b>Irá retornar o voto cadastrado:</b>
<br/>
{
  "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "EmployeeId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "TaskId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "Comment": "string",
  "Date": "2020-11-09T01:42:34.393Z"
}
<br/><br/><br/>

<b>Passo a passo para configurar e utilizar o sistema</b><br/>
- Primeiro realize o clone do projeto
- Após o clone, vá até a pasta .docker e abra um prompt de comando
- Tenha o docker instalado na maquina, caso não tenha, realize o download no seguinte link: https://www.docker.com/get-started
- Depois escreva a seguinte instrução:  docker-compose up -d --build poll-api
- Está instrução irá baixar a imagem do postgree sql e depois irá rodar a imagem do poll-api, onde nele está a nossa API
- Após ter terminado de configurar as imagens, utilize a instrução: docker ps
- Verifique se apareceram duas imagens, com os nomes poll-api e postgres:12, além de ambas estarem com o status Up
- Caso esteja tudo certo, acesso o seguinte link no seu navegador: http://localhost:5033/poll/swagger/index.html
- Caso apareça uma página do swagger com a API poll, significa que está tudo correto, caso contrário, verifique novamente se o seu docker está rodando direitinho
- Após isso, teremos que configurar o banco, a imagem postgres:12 vem com a base poll-pgsql, porém sem nenhuma estrutura com as tabelas, para isso precisaremos restaurar o backup da base
- Mas antes disso, teremos que ter um client de postgres, eu utilizei o pgAdmin, o download pode ser realizado no seguinte link: https://www.pgadmin.org/
- Após instalado, acesse o client e depois crie uma nova conexão
- Irá aparecer uma tela pedindo algumas configurações, informe os seguintes dados:
- Host: localhost
- Port: 5432
- Maintenance Database: postgres
- Username: pguser 
- Password: cmsol@strongpass!123
- Após isso, clique em salvar
- Caso esteja tudo certo, irá aparecer a base de dados do postgres e a pgsql-data, que é a que utilizaremos a partir de agora
- Após isso, vá até a pasta Utils
- Nessa pasta, teremos o arquivo poll-pgsql.bak
- No pgAdmim, clique com o botão direito do mouse na base de dados poll-pgsql > restore
- Irá aparecer uma tela pedindo o arquivo de backup, selecione o arquivo poll-pgsql.bak
- Após isso a sua base estará pronta para utilização
- Caso você não consiga restaurar a base, existe uma outra alternativa, que é utilizar o migration do .net, que pode ser utizada seguindo as instruções do seguinte link: https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
- Agora você pode utilizar o sistema acessando o link http://localhost:5033/poll/swagger/index.html
- Nele contém toda a documentação de cada endpoint, porém já passei o propósito de cada endpoint anteriormente

<br/>

Agora é só utilizar o sistema e se divertir!!!!


