using ControleDeContatos.Repositorio;
using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using SistemaRedeWork.Helper;
using SistemaRedeWork.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Recuperar a string de conex�o do appsettings.json (ajustar se necess�rio)
var connectionString = builder.Configuration.GetConnectionString("MariaDB");

// Configura��o do DbContext para MariaDB
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))
);

// Adicionar servi�os ao container
builder.Services.AddControllersWithViews();

// Registrar os servi�os de depend�ncias para inje��o de depend�ncia (DI)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registrando os reposit�rios e helpers como servi�os escopados
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();

// Configura��o de sess�o
builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true; // Melhor seguran�a para cookies
    options.Cookie.IsEssential = true; // Necess�rio para que as sess�es funcionem corretamente no GDPR
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

// Middleware de autoriza��o
app.UseAuthorization();

// Definir as rotas padr�o do MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Iniciar o aplicativo
app.Run();
