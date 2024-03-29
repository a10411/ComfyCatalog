﻿using ComfyCatalogBOL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ComfyCatalogDAL.Services
{
    /// <summary>
    /// Class que visa implementar todos os métodos de Data Access Layer referentes ao Produto
    /// Isto é, todos os acessos à base de dados relativos ao produto estarão implementados em funções implementadas dentro desta classe
    /// </summary>
    public class ProductService
    {
        #region GET

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter os Produtos
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista dos produtos</returns>

        public static async Task<List<Product>> GetAllProducts(string conString)
        {   
        var productList = new List<Product>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Product", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Product product = new Product(rdr);
                    productList.Add(product);
                }
                rdr.Close();
                con.Close();
            }
            return productList;           
        }

        public static async Task<Product> GetProduct(string conString, int productID)
        {
            Product product = new Product();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Product where productID = {productID}", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    product = new Product(rdr);
                }
                rdr.Close();
                con.Close();
            }
            return product;
            // retorna um produto com id = 0 caso não encontre nenhum com este ID
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e obter um user por username
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>Lista dos produtos</returns>
        public static async Task<User> GetUserByUsername(string conString, string username)
        {
            User user = new User();
            using(SqlConnection con = new SqlConnection( conString ))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM User WHERE username = '{username}'", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user = new User();
                }
                rdr.Close();
                con.Close();
            }

            return user;
        }



        #endregion

        #region POST
        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e adicionar um registo de um produto(adicionar um produto)
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns>True caso tenha adicionado ou retorna a exceção para a camada lógica caso tenha havido algum erro</returns>

        public static async Task<Boolean> AddProduct(string conString, Product productToAdd)
        {
            try
            {
                using (SqlConnection con = new SqlConnection( conString ))
                {
                    string addProduct = "INSERT INTO Product (brandID, estadoID, productName, sport, composition, colour, clientNumber, productType) VALUES (@brandID, @estadoID, @productName, @sport, @composition, @colour, @clientNumber, @productType)";
                    using (SqlCommand queryAddProduct = new SqlCommand(addProduct))
                    {
                        queryAddProduct.Connection = con;
                        queryAddProduct.Parameters.Add("@brandID", SqlDbType.Int).Value = productToAdd.BrandID;
                        queryAddProduct.Parameters.Add("@estadoID", SqlDbType.Int).Value = productToAdd.EstadoID;
                        queryAddProduct.Parameters.Add("@productName", SqlDbType.Char).Value = productToAdd.ProductName;
                        queryAddProduct.Parameters.Add("@sport", SqlDbType.Char).Value = productToAdd.Sport;
                        queryAddProduct.Parameters.Add("@composition", SqlDbType.Char).Value = productToAdd.Composition;
                        queryAddProduct.Parameters.Add("@colour", SqlDbType.Char).Value = productToAdd.Colour;
                        queryAddProduct.Parameters.Add("@clientNumber", SqlDbType.Int).Value =productToAdd.ClientNumber;
                        queryAddProduct.Parameters.Add("@productType", SqlDbType.Char).Value = productToAdd.ProductType;
                        con.Open();
                        queryAddProduct.ExecuteNonQuery();
                        con.Close();
                        return true;

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados via SQL Query e adicionar um novo registo na tabela Favourites consoante o usernae de um user 
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="username">username do cliente a quem se vai adicionar um produto favorito</param>
        /// <returns>True se adicionar, False se não adicionar</returns>
        public static async Task<Boolean> SetFavouriteProductToUser(string conString, string username, int productID)
        {
            try
            {
                User user = await GetUserByUsername(conString, username);
                Product product = await GetProduct(conString, productID);

                using (SqlConnection con = new SqlConnection(conString))
                {
                    string addFavourite = "INSERT INTO Favourite (userID, productID) VALUES (@userID, @productID)";
                    using (SqlCommand queryAddFavourite = new SqlCommand(addFavourite))
                    {
                        queryAddFavourite.Connection = con;
                        queryAddFavourite.Parameters.Add("@userID", SqlDbType.Int).Value = user.UserID;
                        queryAddFavourite.Parameters.Add("@productID", SqlDbType.Int).Value = product.ProductID;

                        con.Open();
                        queryAddFavourite.ExecuteNonQuery();
                        con.Close();
                        return true;
                    }
                }


            }
            catch 
            {
                throw;
            }
        }

        #endregion

        #region PATCH/PUT

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o registo de um producto
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="productUpdated">Produto a atualizar</param>
        /// <returns>Produto atualizado ou erro</returns>

        public static async Task<Product> UpdateProduct(string conString, Product productUpdated)
        {
            Product productCurrent = await GetProduct(conString, productUpdated.ProductID);
            productUpdated.ProductID = productUpdated.ProductID != 0 ? productUpdated.ProductID : productCurrent.ProductID;
            productUpdated.BrandID = productUpdated.BrandID != 0 ? productUpdated.BrandID : productCurrent.BrandID;
            productUpdated.EstadoID = productUpdated.EstadoID != 0 ? productUpdated.EstadoID : productCurrent.EstadoID;
            productUpdated.ProductName = productUpdated.ProductName != String.Empty && productUpdated.ProductName != null ? productUpdated.ProductName : productCurrent.ProductName;
            productUpdated.Sport = productUpdated.Sport != String.Empty && productUpdated.Sport != null ? productUpdated.Sport : productCurrent.Sport;
            productUpdated.Composition = productUpdated.Composition != String.Empty && productUpdated.Composition != null ? productUpdated.Composition : productCurrent.Composition;
            productUpdated.Colour = productUpdated.Colour != String.Empty && productUpdated.Colour != null ? productUpdated.Colour : productCurrent.Colour;
            productUpdated.ClientNumber = productUpdated.ClientNumber != 0 ? productUpdated.ClientNumber : productCurrent.ClientNumber;
            productUpdated.ProductType = productUpdated.ProductType != String.Empty && productUpdated.ProductType != null ? productUpdated.ProductType : productCurrent.ProductType;

            try
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    string updateProduct = "UPDATE Product Set brandID = @brandID, estadoID = @estadoID, productName = @productName, sport = @sport, composition = @composition, colour = @colour, clientNumber = @clientNumber, productType = @productType WHERE productID = @productID";
                    using (SqlCommand queryUpdateProduct = new SqlCommand(updateProduct))
                    {
                        queryUpdateProduct.Connection = con;
                        queryUpdateProduct.Parameters.Add("@productID", SqlDbType.Int).Value = productUpdated.ProductID;
                        queryUpdateProduct.Parameters.Add("@brandID", SqlDbType.Int).Value = productUpdated.BrandID;
                        queryUpdateProduct.Parameters.Add("@estadoID", SqlDbType.Int).Value = productCurrent.EstadoID;
                        queryUpdateProduct.Parameters.Add("productName", SqlDbType.Char).Value = productUpdated.ProductName;
                        queryUpdateProduct.Parameters.Add("@sport", SqlDbType.Char).Value = productUpdated.Sport;
                        queryUpdateProduct.Parameters.Add("@composition", SqlDbType.Char).Value = productUpdated.Composition;
                        queryUpdateProduct.Parameters.Add("@colour", SqlDbType.Char).Value = productUpdated.Colour;
                        queryUpdateProduct.Parameters.Add("@clientNumber", SqlDbType.Int).Value = productUpdated.ClientNumber;
                        queryUpdateProduct.Parameters.Add("@productType", SqlDbType.Char).Value = productUpdated.ProductType;
                        con.Open();
                        queryUpdateProduct.ExecuteNonQuery();
                        con.Close();
                        return await GetProduct(conString, productUpdated.ProductID);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e atualizar o estado de um product
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <param name="productID">ID do produto que pretendemos atualizar</param>
        /// <param name="estadoID">ID do estado para o qual queremos atualizar o produto</param>
        /// <returns>Product com estado atualizado</returns>
        public static async Task<Product> UpdateEstadoProduct(string conString, int productID, int estadoID)
        {
            try
            {
                using (SqlConnection  con = new SqlConnection(conString))
                {
                    string updateEstado = "UPDATE Product SET estadoID = @estadoID WHERE productID = @productID";
                    using (SqlCommand queryUpdateEstado = new SqlCommand(updateEstado))
                    {
                        queryUpdateEstado.Connection = con;
                        queryUpdateEstado.Parameters.Add("@estadoID", SqlDbType.Int).Value = estadoID;
                        queryUpdateEstado.Parameters.Add("@productID", SqlDbType.Int).Value = productID;
                        con.Open();
                        queryUpdateEstado.ExecuteNonQuery();
                        con.Close();
                        return await GetProduct(conString, productID);
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Método que visa aceder à base de dados SQL Server via query e apagar o registo de um produto
        /// </summary>
        /// <param name="conString">String de conexão à base de dados, presente no projeto "ComfyCatalogAPI", no ficheiro appsettings.json</param>
        /// <returns> sucesso ou erro</returns>
        
        public static async Task<Boolean> DeleteProduct(string conString, int productID)
        {
            try
            {
                using(SqlConnection con = new SqlConnection(conString))
                {
                    string deleteProduct = $"DELETE FROM Product WHERE productID = {productID}";
                    using (SqlCommand queryDeleteProduct = new SqlCommand(deleteProduct))
                    {
                        queryDeleteProduct.Connection = con;
                        con.Open();
                        queryDeleteProduct.ExecuteNonQuery();
                        con.Close();
                        return true;

                    }
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

    }
}
