"use strict";
import * as cfg from "./config.js";
import Fastify from "fastify";
import mercurius from "mercurius";
import cors from "@fastify/cors";
import { schema } from "./schema.js";
import { resolvers } from "./resolvers.js";
const app = Fastify();
app.register(cors, {});
app.register(mercurius, {
  schema,
  resolvers,
  graphiql: true, // web page for to test queries
});
app.listen({ port: cfg.port });
//      localhost:5000/graphiql
//      query { alerts {country,name,text,date,region,subregion}}
/* app.post("/graphiql", async (req, res) => {
  try {
    const db = await dbRtns.getDBInstance();
    let allDbUsers = await dbRtns.findAll(db, cfg.collection, {}, {});
    console.log(allDbUsers);
    res.status(200).send({ users: allDbUsers });
  } catch (err) {
    console.log(err.stack);
    res
      .status(500)
      .send({ msg: "get all users failed - internal server error" });
  }
}); */
