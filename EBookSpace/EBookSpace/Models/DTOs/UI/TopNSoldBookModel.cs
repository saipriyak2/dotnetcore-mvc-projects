namespace EBookSpace.Models.DTOs.UI;
public record TopNSoldBookModel(string BookName, string AuthorName, int TotalUnitSold);
public record TopNSoldBooksVm(DateTime StartDate, DateTime EndDate, IEnumerable<TopNSoldBookModel> TopNSoldBooks);
