namespace LearningSystem.Services
{
    public static class ServiceConstants
    {
        public const int ArticlesListingPageSize = 25;

        public const string PdfCertificateLayout = @"
<h1>COURSE CERTIFICATE</h1>
<br />
<h3>This certificate is issued to accknowledge that</h3>
<br />
<h2>{3}</h2>
<br />
<h3>has successfully completed a course</h3>
<br />
<h2>{0} Course</h2>
<br />
<h3>{1} - {2}</h3>
<br />
<h2>with Grade {4}</h2>
<br />
<h4>Signed by {5}</h4>
<h4>Issued on {6}</h4>
";
    }
}
