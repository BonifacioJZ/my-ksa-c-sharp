using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data
{
    public class Pagination<T> : List<T>
    {
        public int initPage {get; private set;}
        public int pageSize {get; private set;}
        public int totalPages {get; private set;}
        public int startPage {get; private set;}
        public int endPage {get; private set;}


        public Pagination(List<T> item,int count,int initPage,int numberOfRecord){
            this.initPage = initPage;
            this.pageSize = (int)Math.Ceiling(count/(double)numberOfRecord);
            this.AddRange(item);
            this.startPage = initPage -5;
            this.endPage = initPage + 4;

            if(startPage<=0){
                this.endPage = this.endPage -(startPage-1);
                this.startPage = 1;
            }

            if(this.endPage>this.pageSize){
                this.endPage = this.pageSize;
                if(this.endPage>10){
                    this.startPage = this.endPage-9;
                }
            }


        }
        public bool previousPages => initPage> 1;
        public bool nextPages => initPage < pageSize;

        public static async Task<Pagination<T>> PaginationCreate(IQueryable<T> origin,int initPage,int numberOfRecord){
            var count = await origin.CountAsync();
            var items = await origin.Skip((initPage - 1)*numberOfRecord).Take(numberOfRecord).ToListAsync();
            return new Pagination<T>(items,count,initPage,numberOfRecord);
        }
    }
}