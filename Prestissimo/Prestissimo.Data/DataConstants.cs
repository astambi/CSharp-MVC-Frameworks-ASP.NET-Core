namespace Prestissimo.Data
{
    public static class DataConstants
    {
        public const int ArticleTitleMinLength = 3;
        public const int ArticleTitleMaxLength = 50;
        public const int ArticleContentsMinLength = 300;

        public const int ArtistNameMaxLength = 100;
        public const int ArtistDescriptionMaxLength = 1000;

        public const int FormatNameMaxLength = 50;
        public const int FormatDescriptionMaxLength = 1000;

        public const int LabelNameMaxLength = 100;
        public const int LabelDescriptionMaxLength = 1000;

        public const double RecordingFormatMinDiscount = 0;
        public const double RecordingFormatMaxDiscount = 100;
        public const double RecordingFormatMinPrice = 0.01;
        public const double RecordingFormatMaxPrice = double.MaxValue;
        public const double RecordingFormatMinQuantity = 0;
        public const double RecordingFormatMaxQuantity = int.MaxValue;

        public const int RecordingDescriptionMaxLength = 1000;
        public const int RecordingMinLength = 1;
        public const int RecordingMaxLength = int.MaxValue;
        public const int RecordingTitleMaxLength = 300;

        public const int UserNameMinLength = 2;
        public const int UserNameMaxLength = 100;
        public const int UserUsernameMaxLength = 50;
    }
}
