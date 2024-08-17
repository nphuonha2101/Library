﻿// <auto-generated />
using System;
using Library.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240816090814_AddBookImage")]
    partial class AddBookImage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("BookCategory", b =>
                {
                    b.Property<long>("BookId")
                        .HasColumnType("bigint")
                        .HasColumnName("book_id");

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint")
                        .HasColumnName("category_id");

                    b.HasKey("BookId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("book_category");
                });

            modelBuilder.Entity("Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("categories");
                });

            modelBuilder.Entity("Library.Entities.Implements.Author", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("dob");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("full_name");

                    b.HasKey("Id");

                    b.ToTable("authors");
                });

            modelBuilder.Entity("Library.Entities.Implements.Book", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("BookImage")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("book_image");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<DateTime>("ImportedDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("import_date");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("isbn");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("books");
                });

            modelBuilder.Entity("Library.Entities.Implements.BookAuthor", b =>
                {
                    b.Property<long>("BookId")
                        .HasColumnType("bigint")
                        .HasColumnName("book_id");

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint")
                        .HasColumnName("author_id");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("book_author");
                });

            modelBuilder.Entity("Library.Entities.Implements.Loan", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("LoanDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("loan_date");

                    b.Property<long?>("LoanFineId")
                        .HasColumnType("bigint")
                        .HasColumnName("loan_fine_id");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("LoanFineId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("loans");
                });

            modelBuilder.Entity("Library.Entities.Implements.LoanDetail", b =>
                {
                    b.Property<long>("LoanId")
                        .HasColumnType("bigint")
                        .HasColumnName("loan_id");

                    b.Property<long>("BookId")
                        .HasColumnType("bigint")
                        .HasColumnName("book_id");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("due_date");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("ReturnDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("return_date");

                    b.HasKey("LoanId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("loan_details");
                });

            modelBuilder.Entity("Library.Entities.Implements.LoanFine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("double")
                        .HasColumnName("amount");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_date");

                    b.Property<long>("LoanId")
                        .HasColumnType("bigint")
                        .HasColumnName("loan_id");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("payment_status");

                    b.HasKey("Id");

                    b.HasIndex("LoanId");

                    b.ToTable("fines");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("address");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("dob");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_admin");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("BookCategory", b =>
                {
                    b.HasOne("Library.Entities.Implements.Book", "Book")
                        .WithMany("BookCategories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Category", "Category")
                        .WithMany("BookCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Library.Entities.Implements.BookAuthor", b =>
                {
                    b.HasOne("Library.Entities.Implements.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.Implements.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Library.Entities.Implements.Loan", b =>
                {
                    b.HasOne("Library.Entities.Implements.LoanFine", "LoanFine")
                        .WithOne()
                        .HasForeignKey("Library.Entities.Implements.Loan", "LoanFineId");

                    b.HasOne("User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoanFine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.Entities.Implements.LoanDetail", b =>
                {
                    b.HasOne("Library.Entities.Implements.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.Implements.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("Library.Entities.Implements.LoanFine", b =>
                {
                    b.HasOne("Library.Entities.Implements.Loan", "Loan")
                        .WithMany()
                        .HasForeignKey("LoanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loan");
                });

            modelBuilder.Entity("Category", b =>
                {
                    b.Navigation("BookCategories");
                });

            modelBuilder.Entity("Library.Entities.Implements.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("Library.Entities.Implements.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("BookCategories");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
