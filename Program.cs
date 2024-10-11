using ControleDeContatos.Repositorio;
using Microsoft.EntityFrameworkCore;
using SistemaRedeWork.Data;
using SistemaRedeWork.Helper;
using SistemaRedeWork.Repositorio;

var builder = WebApplication.CreateBuilder(args);

// Recuperar a string de conexão do appsettings.json (ajustar se necessário)
var connectionString = builder.Configuration.GetConnectionString("MariaDB");

// Configuração do DbContext para MariaDB
builder.Services.AddDbContext<BancoContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)))
);

// Adicionar serviços ao container
builder.Services.AddControllersWithViews();

// Registrar os serviços de dependências para injeção de dependência (DI)
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Registrando os repositórios e helpers como serviços escopados
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();

// Configuração de sessão
builder.Services.AddSession(options => {
    options.Cookie.HttpOnly = true; // Melhor segurança para cookies
    options.Cookie.IsEssential = true; // Necessário para que as sessões funcionem corretamente no GDPR
});

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Habilitar HSTS para ambientes de produção
}

// Middleware para redirecionamento HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Habilitar o roteamento
app.UseRouting();

// Habilitar sessões
app.UseSession();

// Middleware de autorização
app.UseAuthorization();

// Definir as rotas padrão do MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// Iniciar o aplicativo
app.Run();
