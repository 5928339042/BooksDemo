﻿using BooksDemo.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksDemo.Migrations; 

public class MigrationHelper {
    public static async Task ApplyDatabaseMigrationsAsync(IServiceProvider services) {
        using var scope = services.CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger<MigrationHelper>();
        var hostLifetime = services.GetRequiredService<IHostApplicationLifetime>();

        logger.LogInformation("Applying database migrations...");
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryContext>();
        await dbContext.Database.MigrateAsync(hostLifetime.ApplicationStopping);
        logger.LogInformation("Database migrations have been applied.");
    }
}