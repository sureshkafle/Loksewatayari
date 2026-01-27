# To create migration, go to root
`dotnet ef migrations add Initial -o Data/Migrations/  --startup-project WebApi/ --project QuizCore`

# Create script for current migration from root
`dotnet ef migrations script --project QuizCore`

# Create the razor page
`dotnet new page -n Result -o Pages`

# Create Class Library
`dotnet new classlib -n LokFrontend.Application`
`dotnet new classlib -n LokFrontend.Infrastructure`
