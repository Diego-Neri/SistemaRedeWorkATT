using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;


var builder = WebApplication.CreateBuilder(args);

// Recuperar a string de conex�o do appsettings.json (ajustar se necess�rio)
var connectionString = builder.Configuration.GetConnectionString("MariaDB");

// Configura��o do DbContext para MariaDB
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))
);

// Adicionar servi�os ao container
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();


// Registrar os servi�os de depend�ncias para inje��o de depend�ncia (DI)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        //Empresas
        options.LoginPath = "/Login/EmpresaLogado"; // Caminho para a página de login
        options.LogoutPath = "/LoginEmpresa/Logout"; // Caminho para a página de logout
        options.AccessDeniedPath = "/Home/AcessoNegado"; // Caminho para a página de acesso negado

        //Estudantes
        options.LoginPath = "/Login/_EstudanteLogado"; // Caminho para a página de login
        options.LogoutPath = "/LoginEmpresa/Logout"; // Caminho para a página de logout
        options.AccessDeniedPath = "/Home/AcessoNegado"; // Caminho para a página de acesso negado
    }); ;

// Adicionar serviços de sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de expiração da sessão
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configura��o do pipeline de requisi��o HTTP
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Habilitar HSTS para ambientes de produ��o
}

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Habilitar o roteamento
app.UseRouting();

// Habilitar sess�es
app.UseSession();

// Habilitar autenticação e autorização
app.UseAuthentication(); // Adicionar autenticação antes da autorização
app.UseAuthorization();

// Definir as rotas padr�o do MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Iniciar o aplicativo
app.Run();
