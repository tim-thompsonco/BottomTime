<h2>Bottom Time API</h2>
<p>This is a .NET Core Web API that I am building both as a means to back up my physical dive log and also to deepen my skill set with .NET. It is designed using the OpenAPI / Swagger v1 specification. This API will allow for CRUD operations pertaining to dives for a particular user. Once there is basic functionality in place for a user to consume the API, I will post API documentation here.</p>

<h2>Build Pipeline</h2>
<p>Acceptance environment points to https://bottomtimeapi-acc.herokuapp.com/swagger/index.html.</p>
<p>Production environment points to https://bottomtimeapi-prod.herokuapp.com/swagger/index.html.</p>
<p>For all code changes, a branch is cut off acceptance for each ticket, which is then squashed and merged into acceptance once the ticket is complete. The acceptance environment is then tested first prior to merging acceptance into production to avoid downtime from breaking changes.</p>

<h2>Built With</h2>
  <ul>
    <li>C#
    <li>.NET Core
    <li>Entity Framework Core
    <li>PostgreSQL
    <li>Heroku
  </ul>

<h2>Authors</h2>
<p>Tim Thompson</p>

<h2>Acknowledgments</h2>
<p>Made possible by the Build An App With ASPNET Core And Angular From Scratch course on Udemy by Neil Cummings.</p>
