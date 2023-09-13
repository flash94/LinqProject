using LinqProject;

var lawyers = new[]
{
    new Lawyer()
    {
        FirstName = "John",
        LastName = "Vintage"
    },
    new Lawyer()
    {
        FirstName = "Enoch",
        LastName = "Light"
        
    }
};

var clients = new[]
{
    new Client()
    {
        FirstName = "Tim",
        LastName = "Cook"
    },
    new Client()
    {
        FirstName = "Ilesanmi",
        LastName = "James"
    },
    new Client()
    {
        FirstName = "Maria",
        LastName = "White"
    }
};

var cases = new[]
{
    new Case()
    {
        Title = "Car Accident",
        AmountInDispute = 50000,
        CaseType = CaseType.Commercial,
        Client = clients[0],
        Lawyer = lawyers[0]
    },

    new Case()
    {
        Title = "Death threat",
        AmountInDispute = 50000,
        CaseType = CaseType.ProBono,
        Client = clients[2],
        Lawyer = lawyers[0]
    },
    new Case()
    {
        Title = "Presidential Election",
        AmountInDispute = 500000,
        CaseType = CaseType.Commercial,
        Client = clients[1],
        Lawyer = lawyers[1]
    }
};

//Where
foreach(Lawyer lawyer in lawyers)
    lawyer.Cases = cases.Where(c => c.Lawyer == lawyer).ToList();

foreach(Client client in clients)
    client.Cases = cases.Where(c => c.Client == client).ToList();

//First and single
var workingFirstExample = lawyers.First(l => l.FirstName == "John");

try
{
    var firstExceptionExample = lawyers.First(l => l.FirstName == "Ade");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine("Invalid operation exception has been thrown, cause no matching element found.");
}

//FirstOrDefault returns the default value for the specified datatype, if no matching element was found.
//For classes thats null and for value types thats the default value. for int it is 0 for example.
var firstOrDefaultExample = lawyers.FirstOrDefault(l => l.FirstName == "Ade");

//Single works like first, but ensures, that only a sinle element matches the specified condition
var workingSingleExample = lawyers.Single(l => l.FirstName == "John");

//SingleOrDefault returns the default value for the specified datatype, if no matching element was found.
//For classes thats null and for value types thats the default value. for int it is 0 for example.
//Everything else works just like single

var singleOrDefaultExample = lawyers.SingleOrDefault(l => l.FirstName == "joh");

//Any and All
var proBonoLawyers = lawyers.Where(l => l.Cases.Any(c => c.CaseType == CaseType.ProBono));
var commercialOnlyLawyers = lawyers.Where(l => l.Cases.All(c => c.CaseType == CaseType.Commercial));

//working with numbers
var sumOfAmountInDispute = cases.Sum(c => c.AmountInDispute);
var averageAmountInDispute = cases.Average(c => c.AmountInDispute);
var maxAmountInDispute = cases.Max(c => c.AmountInDispute);
var minAmountInDipute = cases.Min(c => c.AmountInDispute);

//select
var caseTitles = cases.Select(c => c.Title);
var lawyerNames = lawyers.Select(l => l.FirstName + "," + l.LastName);
//Select returns a list of lists here
var casesPerLawyer = lawyers.Select(l => l.Cases);
//selectmany returns a flattened list
var casesPerLawyerFlat = lawyers.SelectMany(l => l.Cases);

//Fluent - Chaining Linq Queries
var caseTitlesOfCommercialOnlyLawyers = lawyers
    .Where(l => l.Cases.All(c => c.CaseType == CaseType.Commercial))
    .SelectMany(l => l.Cases)
    .Select(c => c.Title);