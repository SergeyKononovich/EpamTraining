using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BL.IDAL.IRepositories;
using BL.Models;
using Data;

namespace DAL.Repositories
{
    public class SaleRepository : RepositoryBase<Data.Models.SaleModel, BL.Models.SaleModel>, ISaleRepository
    {
        public SaleRepository(StoreContext context)
            : base(context)
        {
        }


        public SalesPage GetPage(SaleFilter filter, SalesSortingOptions sortOpt, int skip, int take)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));

            var items = GetItemsProcessedByFilters(filter);
            var orderedItems = Order(items, sortOpt);
            var displayedItems = orderedItems.Skip(skip).Take(take).ToList();

            var page = new SalesPage
            {
                Displayed = Mapper.Map<IList<SaleModel>>(displayedItems),
                TotalRecords = Context.Sales.Count(),
                TotalDisplayRecords = items.Count()
            };

            return page;
        }

        private IQueryable<Data.Models.SaleModel> GetItemsProcessedByFilters(SaleFilter f)
        {
            var items = Context.Sales.Where(m =>
                m.Id >= f.FromId && m.Id <= f.ToId &&
                m.Date >= f.FromDate && m.Date <= f.ToDate &&
                (f.GoodsNamePart == null || m.Goods.Name.Contains(f.GoodsNamePart)) &&
                (f.ManagerSecondNamePart == null || m.Manager.SecondName.Contains(f.ManagerSecondNamePart)) &&
                (f.ClientFullNamePart == null || m.Client.FullName.Contains(f.ClientFullNamePart)) &&
                m.SellingPrice >= f.FromPrice && m.SellingPrice <= f.ToPrice);

            if (f.Search != null)
            {
                var s = f.Search.Split(' ').ToList();
                items = items.Where(m => s.Any(a => m.Id.ToString().Contains(a)) ||
                                         s.Any(a => m.Date.ToString().Contains(a)) ||
                                         s.Any(a => m.Goods.Name.Contains(a)) ||
                                         s.Any(a => m.Manager.SecondName.Contains(a)) ||
                                         s.Any(a => m.Client.FullName.Contains(a)) ||
                                         s.Any(a => m.SellingPrice.ToString().Contains(a)));
            }

            return items;
        }

        private static IOrderedQueryable<Data.Models.SaleModel> Order(IQueryable<Data.Models.SaleModel> items,
            SalesSortingOptions sortOpt)
        {
            IOrderedQueryable<Data.Models.SaleModel> ordItems;

            if (sortOpt.Options.Count == 0)
                ordItems = items.OrderBy(m => m.Id);
            else
            {
                var first = sortOpt.Options[0];
                switch (first.Column)
                {
                    case SaleOrderColumn.Id:
                        ordItems = first.Ascending
                            ? items.OrderBy(m => m.Id)
                            : items.OrderByDescending(m => m.Id);
                        break;
                    case SaleOrderColumn.Date:
                        ordItems = first.Ascending
                            ? items.OrderBy(m => m.Date)
                            : items.OrderByDescending(m => m.Date);
                        break;
                    case SaleOrderColumn.Goods:
                        ordItems = first.Ascending
                            ? items.OrderBy(m => m.Goods.Name)
                            : items.OrderByDescending(m => m.Goods.Name);
                        break;
                    case SaleOrderColumn.Manager:
                        ordItems = first.Ascending
                            ? items.OrderBy(m => m.Manager.SecondName)
                            : items.OrderByDescending(m => m.Manager.SecondName);
                        break;
                    case SaleOrderColumn.Client:
                        ordItems = first.Ascending
                            ? items.OrderBy(m => m.Client.FullName)
                            : items.OrderByDescending(m => m.Client.FullName);
                        break;
                    case SaleOrderColumn.SellingPrice:
                        ordItems = first.Ascending
                            ? items.OrderBy(m => m.SellingPrice)
                            : items.OrderByDescending(m => m.SellingPrice);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (var o in sortOpt.Options)
            {
                switch (o.Column)
                {
                    case SaleOrderColumn.Id:
                        ordItems = o.Ascending
                            ? ordItems.ThenBy(m => m.Id)
                            : ordItems.ThenByDescending(m => m.Id);
                        break;
                    case SaleOrderColumn.Date:
                        ordItems = o.Ascending
                            ? ordItems.ThenBy(m => m.Date)
                            : ordItems.ThenByDescending(m => m.Date);
                        break;
                    case SaleOrderColumn.Goods:
                        ordItems = o.Ascending
                            ? ordItems.ThenBy(m => m.Goods.Name)
                            : ordItems.ThenByDescending(m => m.Goods.Name);
                        break;
                    case SaleOrderColumn.Manager:
                        ordItems = o.Ascending
                            ? ordItems.ThenBy(m => m.Manager.SecondName)
                            : ordItems.ThenByDescending(m => m.Manager.SecondName);
                        break;
                    case SaleOrderColumn.Client:
                        ordItems = o.Ascending
                            ? ordItems.ThenBy(m => m.Client.FullName)
                            : ordItems.ThenByDescending(m => m.Client.FullName);
                        break;
                    case SaleOrderColumn.SellingPrice:
                        ordItems = o.Ascending
                            ? ordItems.ThenBy(m => m.SellingPrice)
                            : ordItems.ThenByDescending(m => m.SellingPrice);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return ordItems;
        }
    }
}