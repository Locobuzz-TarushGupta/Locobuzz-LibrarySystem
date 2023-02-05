﻿using library_management_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Text.Json;
using library_management_system.Controllers;
using library_management_system.DatabaseLayer;

namespace library_management_system.Controllers.Logic
{
    public class LogicLibrarian
    {

        //     public async virtual Task<string> DbGetAllBooks() { return ""; }
        DbLibrarian librarian = new DbLibrarian();
        [HttpGet]
        public string GetBooksLogic()
        {
            string result = librarian.DbGetAllBooks();
            return result;
        }

        [HttpPost]
        public async Task<string> AddBookLogic(string jsonFile)
        {
            BookDetails book = JsonSerializer.Deserialize<BookDetails>(jsonFile);
            string result = await librarian.DbAddBook(book);
            return result;

        }

        [HttpPost, HttpPut] 
        public async Task<string> UpdateBookLogic(string jsonFile)
        {
            BookDetails book = JsonSerializer.Deserialize<BookDetails>(jsonFile);
            string result = await librarian.DbUpdateBook(book);
            return result;

        }

        [HttpPut, HttpPost, HttpDelete]
        public async Task<string> DeleteBookLogic(string BookId)
        {
         //   dynamic book = JsonSerializer.Deserialize<dynamic>(jsonFile);
         //   string BookId = book["BookId"];
            string result = await librarian.DbDeleteBook(BookId);
            return result;

        }

        [HttpPost, HttpPut]
        public async Task<string> IssuedBooksLogic()
        {
            string result = await librarian.DbIssuedBooks();
            return result;
        //    BookDetails books = JsonSerializer.Deserialize<BookDetails>(result);
        //    return books;
        }

        [HttpPost, HttpPut]
        public async Task<string> DueBooksLogic()
        {
            string result = await librarian.DbDueBooks();
            return result;
        }
    }
}
