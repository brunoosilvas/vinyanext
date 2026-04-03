namespace Vinyanext.Domain.Abstractions.Services;

public interface IPaginatorService
{
    int GetPage(int page, int totalByPage);

    int GetPagePgsql(int page, int totalByPage);

    (int maxPage, int total, int totalRegisterByPage) GetMetadata(
        int total, int totalRegisterByPage
    );
}
