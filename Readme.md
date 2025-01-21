# To create migration, go to root
`dotnet ef migrations add Initial -o Data/Migrations/  --startup-project WebApi/ --project QuizCore`

# Create script for current migration from root
`dotnet ef migrations script --project QuizCore`