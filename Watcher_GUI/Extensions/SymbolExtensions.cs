using Microsoft.Azure.Devices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Watcher_GUI
{
    public static class SymbolExtensions
    {
        /// <summary>
        /// Add Symbol To Database
        /// </summary>
        /// <param name="db"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static async Task<Symbol> AddSymbol(this DatabaseContext db, Symbol symbol)
        {
            symbol.ToLower();
            db.Symbols.Add(symbol);
            await db.SaveChangesAsync();
            return symbol;
        }

        /// <summary>
        /// Delete Symbol from Database
        /// </summary>
        /// <param name="db"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static async Task<Symbol> DeleteSymbol(this DatabaseContext db, Symbol symbol)
        {
            db.Symbols.Remove(symbol);
            await db.SaveChangesAsync();
            return symbol;
        }

        /// <summary>
        /// Get a Symbol using the Symbol name
        /// </summary>
        /// <param name="db"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static async Task<Symbol> GetSymbol(this DatabaseContext db, string name)
        {
            var symbol = await db.Symbols.FirstOrDefaultAsync(x => x.SymbolName == name.ToLower());
            return symbol != null ? symbol : null;
        }
    }
}
