var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("trash-services")
                      .WithDataVolume(isReadOnly: false);

var db = postgres.AddDatabase("trash-services-db");

builder.AddProject<Projects.TrashService>("trashservice")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
