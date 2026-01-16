using DicomValidator.Rules;
using DicomValidator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// register rules
builder.Services.AddSingleton<IValidationRule, RequiredTagsRule>();
builder.Services.AddSingleton<IValidationRule, UidRule>();
builder.Services.AddSingleton<IValidationRule, PixelDataRule>();
builder.Services.AddSingleton<IValidationRule, ModalitySopRule>();
builder.Services.AddSingleton<IValidationRule, PrivateTagRule>();

// core services
builder.Services.AddSingleton<DicomValidationService>();
builder.Services.AddSingleton<DicomDeidentificationService>();

builder.Services.AddEndpointsApiExplorer();
// register rules builder.Services.AddSingleton<IValidationRule, RequiredTagsRule>(); builder.Services.AddSingleton<IValidationRule, UidRule>(); builder.Services.AddSingleton<IValidationRule, PixelDataRule>(); builder.Services.AddSingleton<IValidationRule, ModalitySopRule>(); builder.Services.AddSingleton<IValidationRule, PrivateTagRule>(); // core services builder.Services.AddSingleton<DicomValidationService>(); builder.Services.AddSingleton<DicomDeidentificationService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
