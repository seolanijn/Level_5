import { Router } from "express";
const router = Router();
// define a default route
router.get("/", (req, res) => {
  res
    .status(200)
    .send({ msg: `this would be a response from the default route` });
});
// define a get route with a name parameter
router.get("/:name", (req, res) => {
  let name = req.params.name;
  res
    .status(200)
    .send({ msg: `this would be a response using the ${name} parameter` });
});
export default router;
