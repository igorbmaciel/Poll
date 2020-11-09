# Poll

Projeto Realizado para o desafio da Alterdata

<b>Qual arquitetura foi escolhida?</b><br/>
R - Foi utilizado o .net core 2.2 com o conceito de DDD e utilizando command e handler, além de estar rodando no docker.

<b>Porque foi escolhido essa arquitetura?</b><br/>
R- Foi escolhida pois o .net core é um cross plataform, senod facilemnte colocado junto ao docker em qualquer ambiente, além do conceito de DDD utilizadon command e handler, separando cada responsabilidade, dando facil entidmento e manutenção do código, bem como seu ciclo de vida facilmente utilizável.

<b>Quais endpoints foram desenvolvidos?</b><br/>
R- Foram cridos três endpoints para a finalidade de criar funcionários, tarefas e a votação em si.

<b>Explicando cada endpoint</b>

O Primeiro endpoint é o de Employee, com os seguintes métodos:<br/>

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

O Segundo endpoint é o de Task, com os seguintes métodos:<br/><br/>

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
<b>Irá retornar uma lista de tarefas com os usuários e a hora que eles votaram:</b><br/>
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
