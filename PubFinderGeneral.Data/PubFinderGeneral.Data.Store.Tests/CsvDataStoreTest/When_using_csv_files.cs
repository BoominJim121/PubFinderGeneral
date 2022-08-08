using FluentAssertions;

namespace PubFinderGeneral.Data.Store.Tests.CsvDataStoreTest
{
    public class When_using_csv_files
    {
        [Fact]
        public async Task Given_a_standard_csv_file_with_multiple_lines()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest","TestFiles");
            var fileName = "StandardFile.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1, 5);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().NotBeEmpty();
            result.Item1.Count().Should().Be(4);
            result.Item1.FirstOrDefault().Should().NotBeNull();
            result.Item1.FirstOrDefault()?.Name.Should().NotBe(string.Empty);

        }

        [Fact]
        public async Task Given_a_csv_file_with_full_stops_in_the_name_and_excerpt()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest", "TestFiles");
            var fileName = "NameAndDescriptionHadFullStops.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1, 5);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().NotBeEmpty();
            result.Item1.Count().Should().Be(1);
            result.Item1.FirstOrDefault().Should().NotBeNull();
            result.Item1.FirstOrDefault()?.Name.Should().NotBe(string.Empty);
            result.Item1.FirstOrDefault()?.Name.Should().Contain("...");
            result.Item1.FirstOrDefault()?.Excerpt.Should().Contain("...");

        }

        [Fact]
        public async Task Given_a_csv_file_with_quotes_stops_in_the_name_and_excerpt()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest", "TestFiles");
            var fileName = "NameAndDescriptionHaveQuotes.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1, 5);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().NotBeEmpty();
            result.Item1.Count().Should().Be(1);
            result.Item1.FirstOrDefault().Should().NotBeNull();
            result.Item1.FirstOrDefault()?.Name.Should().NotBe(string.Empty);
            result.Item1.FirstOrDefault()?.Name.Should().Contain("\"");
            result.Item1.FirstOrDefault()?.Excerpt.Should().Contain("\"");

        }

        [Fact]
        public async Task Given_a_csv_file_with_ratings_that_are_decimals()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest", "TestFiles");
            var fileName = "RatingsContainDecimals.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1, 5);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().NotBeEmpty();
            result.Item1.Count().Should().Be(1);
            result.Item1.FirstOrDefault().Should().NotBeNull();
            result.Item1.FirstOrDefault()?.About.Ratings.Any(x => x.Name == "Beer").Should().BeTrue();
            result.Item1.FirstOrDefault()?.About.Ratings.FirstOrDefault(x => x.Name == "Beer")?.Value.Should().Be(Convert.ToDecimal(1.5));

        }


        [Fact]
        public async Task Given_a_csv_file_with_none_standard_ascii_characters()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest", "TestFiles");
            var fileName = "FieldsHaveNoneASCIICharacters.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1,5);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().NotBeEmpty();
            result.Item1.Count().Should().Be(1);
            result.Item1.FirstOrDefault().Should().NotBeNull();
            result.Item1.FirstOrDefault()?.Name.Should().NotBe(string.Empty);
            result.Item1.FirstOrDefault()?.Name.Should().Be("Arts Café日本人 中國的"); 

        }

        [Fact]
        public async Task Given_a_csv_file_with_many_values_total_page_size_is_calculated_correctly()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest", "TestFiles");
            var fileName = "leedsbeerquest.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1, 2);

            result.Item2.Should().NotBe(0);
            result.Item2.Should().Be(121);
        }

        [Fact]
        public async Task Given_a_csv_file_with_many_values_total_page_size_is_calculated_correctly_when_pages_not_all_full()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "CsvDataStoreTest", "TestFiles");
            var fileName = "leedsbeerquest.csv";

            var store = new CsvPubDataStore(path, fileName);

            var result = await store.GetAllPubData(1, 8);

            result.Item2.Should().NotBe(0);
            result.Item2.Should().Be(31);

            result = await store.GetAllPubData(31, 8);

            result.Item1.Should().NotBeNull();
            result.Item1.Should().NotBeEmpty();
            result.Item1.Count().Should().Be(2);
        }
    }
}