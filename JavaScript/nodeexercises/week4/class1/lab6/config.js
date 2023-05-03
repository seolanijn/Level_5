import { config } from "dotenv";
config();
export const url = process.env.ISOCOUNTRIES;
export const atlas = process.env.DBURL;
export const appdb = process.env.DB;
export const coll = process.env.COLLECTION;
