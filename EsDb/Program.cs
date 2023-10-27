using EsDb.Domain;
using EsDb.Infra;
using EventStore.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBusinessDocumentRepository, BusinessDocumentRepository>();
builder.Services.AddScoped<IQueryBusinessDocuments, QueryBusinessDocuments>();
builder.Services.AddScoped<ICommandBusinessDocuments, CommandBusinessDocuments>();
builder.Services.AddEventStoreClient( 
    (uri) => "esdb://127.0.0.1:2113?tls=false", 
    (it) => it.CreateHttpMessageHandler = () =>
    new HttpClientHandler {
        ServerCertificateCustomValidationCallback =
            (sender, cert, chain, sslPolicyErrors) => true
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();