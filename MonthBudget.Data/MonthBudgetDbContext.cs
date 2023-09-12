using Microsoft.EntityFrameworkCore;
using MonthBudget.Data.Models;

namespace MonthBudget.Data;

public partial class MonthBudgetDbContext : DbContext
{
    public MonthBudgetDbContext()
    {
    }

    public MonthBudgetDbContext(DbContextOptions<MonthBudgetDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<MonthlyGoal> MonthlyGoals { get; set; }

    public virtual DbSet<RecurringExpense> RecurringExpenses { get; set; }

    public virtual DbSet<RecurringIncome> RecurringIncomes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83FB4FF0AB2");

            entity.ToTable("accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("accountName");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FD8C4351A");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("categoryName");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.UserId, e.CategoryId }).HasName("PK__expenses__3213E83FBBF64C8C");

            entity.ToTable("expenses");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdOn");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Note)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("note");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("title");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("transactionDate");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("datetime")
                .HasColumnName("updatedOn");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__income__3213E83F168BED49");

            entity.ToTable("income");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdOn");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Note)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("note");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("source");
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("transactionDate");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("datetime")
                .HasColumnName("updatedOn");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<MonthlyGoal>(entity =>
        {
            entity.ToTable("monthlyGoals");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Month)
                .HasDefaultValueSql("((1))")
                .HasColumnName("month");
            entity.Property(e => e.Type)
                .HasDefaultValueSql("((1))")
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.Year)
                .HasDefaultValueSql("((2023))")
                .HasColumnName("year");
        });

        modelBuilder.Entity<RecurringExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recurrin__3213E83F6C38C0E7");

            entity.ToTable("recurringExpenses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CategoryId).HasColumnName("categoryId");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("('getdate()')")
                .HasColumnType("datetime")
                .HasColumnName("createdOn");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("('getdate()')")
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Note)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("note");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("('getdate()')")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("title");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<RecurringIncome>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recurrin__3213E83F939840DC");

            entity.ToTable("recurringIncome");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("('getdate()')")
                .HasColumnType("datetime")
                .HasColumnName("createdOn");
            entity.Property(e => e.EndDate)
                .HasDefaultValueSql("('getdate()')")
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Note)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("note");
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("source");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("('getdate()')")
                .HasColumnType("datetime")
                .HasColumnName("startDate");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F161EBC32");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("avatar");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
