using efcoreApp.Data;
using Microsoft.EntityFrameworkCore;

public class StudentService
{
    private readonly efcoreAppContext _context;

    public StudentService(efcoreAppContext context)
    {
        _context = context;
    }

    public async Task UpdateStudentLevelsAsync()
    {
        var ogrenciler = await _context.Ogrenciler.Include(o => o.VideoWatchedChecks).ToListAsync();

        foreach (var ogrenci in ogrenciler)
        {
            int watchedCount = ogrenci.VideoWatchedChecks.Count(v => v.Watched);
            ogrenci.Level = watchedCount / 10;
        }

        await _context.SaveChangesAsync();
    }
}
