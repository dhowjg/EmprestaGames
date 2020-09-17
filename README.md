# EmprestaGames

#tabelas 

Pessoas - todas as pessoas cadastradas
Jogos - todos os jogos cadastrados
PessoaXJogos - relacionamento da pessoas x jogos
Emprestimo - onde ficara as datas de emprestimo e com quem está o jogo


SCRIPTS 


dotnet new webapi -o EmprestaGames.Api
dotnet add package Microsoft.AspNetCore.Authentication
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet watch run
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet ef database drop
dotnet ef migrations remove
dotnet build  


webservicess

https://localhost:5001/v1/account/login
body 
{
	"Nome": "admin",
	"Senha" : "admin"
}