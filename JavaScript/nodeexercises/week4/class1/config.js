import { config } from "dotenv";
config();
export const atlas = process.env.DBURL;
export const appdb = process.env.DB;
