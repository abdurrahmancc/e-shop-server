using e_shop_server.Controllers.Products;
using e_shop_server.Models;
using e_shop_server.Services;
using e_shop_server.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<ProductService>();
builder.Services.AddAutoMapper(typeof (Program));
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(Options=>{
    Options.InvalidModelStateResponseFactory = context=>{
        var errors = context.ModelState
                .Where(e=> e.Value != null && e.Value.Errors.Count> 0)
                .SelectMany(e=> e.Value?.Errors != null ? e.Value.Errors.Select(x=>x.ErrorMessage): new List<string>()).ToList() ;

                return new BadRequestObjectResult(ApiResponse<Object>.ErrorResponse(errors, 400, "Validation error"));
    };
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/",()=>"Api is working fine");

app.MapControllers();
app.Run();


