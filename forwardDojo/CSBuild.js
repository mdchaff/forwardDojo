var projectPath = "Users,mdchf,Desktop,forwardDojo".split(",").join("\\");
var projectName = "forwardDojo";
str = "";
// =======================================================

function start(str){
    str += "cd ..\\.. & cd \""+projectPath+"\" & ";
    str += "dotnet new web -o "+projectName+" & ";
    str += "cd "+projectName+" & ";
    str += "setx ASPNETCORE_ENVIRONMENT Development & "
    return fileUpdate(str);
}
function fileUpdate(str){
    projectPath = projectPath.split("\\"); projectPath = projectPath[0]+"\\"+projectPath[1]+"\\"+projectPath[2]+"\\CSBuild\\";
    str += "mkdir Views & cd Views & mkdir Home & ";
    str += "echo //Auto_Built > _Viewimports.cshtml & type "+projectPath+"project_csproj_File.txt >> _Viewimports.cshtml & cd.. & ";
    str += "mkdir Controllers & cd Controllers & echo //Auto_Built > HomeController.cs & type "+projectPath+"controllerFile.txt >> HomeController.cs & cd.. & ";
    str += "mkdir Models & cd Models & echo //Auto_Built > MyContext.cs & type "+projectPath+"contextFile.txt >> MyContext.cs & cd.. & ";

    // str += "echo //Auto_Built > "+projectName+".csproj & type "+projectPath+"project_csproj_File.txt >> "+projectName+".csproj & ";
    str += "echo //Auto_Built > appsettings.json & type "+projectPath+"appsettingFile.txt >> appsettings.json & ";
    str += "del /f Program.cs & echo //Auto_Built > Program.cs & type "+projectPath+"programFile.txt >> Program.cs & ";
    str += "del /f Startup.cs & echo //Auto_Built > Startup.cs & type "+projectPath+"startupFile.txt >> Startup.cs & ";
    str += "code . & dotnet watch run"
    return str;
}
console.log(start(""));




// function project_csproj_File(){}
// function controllerFile(){}
// function contextFile(){}
// function project_csproj_File(){}
// function appsettingFile(){}
// function programFile(){}
// function startupFile(){}