'use strict';
import dotenv from 'dotenv';
import { Sequelize } from 'sequelize';
dotenv.config();
const db = new Sequelize(process.env.DB_NAME, process.env.DB_USER, process.env.DB_PASSWORD, {
   dialect: process.env.DB_DIALECT,
   host: process.env.DB_HOST,
   port: process.env.DB_PORT,
   define: {
      timestamps: false
  }
});

db.authenticate()
  .then(() => {
    console.log(`[${new Date().toLocaleString()}]   : DB Module : Db connect successfully`);
  })
  .catch((error) => {
    (error instanceof Sequelize.ConnectionError) ? console.log(`[${new Date().toLocaleString()}] : DB module : Service is unavailable. Retry again ...`) 
                                                 : console.log(`[${new Date().toLocaleString()}] : DB module : An error occurred during database authentication.\n Reason: Please check your access rules or login credentials`);
  });

export default db;
