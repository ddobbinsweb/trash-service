var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithPgAdmin()
    .WithDataVolume(isReadOnly: false);

var db = postgres.AddDatabase("trash-services-db");

builder.AddProject<Projects.TrashService>("trashservice-web")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
