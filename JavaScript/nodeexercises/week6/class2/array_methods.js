// find even numbers
const allnumbers1 = [1, 2, 3, 4];
let newarray = allnumbers1.filter((el) => el % 2 === 0); // [2, 4]
console.table(newarray);
// sum all numbers
const allnumbers2 = [1, 2, 3, 4, 5];
let total = allnumbers2.reduce((total, item) => total + item, 0); // 15
console.log(total);
// existence check
const allnumbers3 = [1, 2, 3, 4, 5];
let exists = allnumbers3.includes(3);
exists
  ? console.log(`there is a 3 in the array `)
  : console.log(`no 3 in the array`);
// put a new element at the start
const allnumbers4 = [1, 2, 3, 4, 5];
allnumbers4.unshift(0);
console.table(allnumbers4); // [0, 1, 2, 3, 4, 5]
// Build a custom string
const allnumbers5 = [1, 2, 3, 4, 5];
let custom_string = allnumbers5.join(", ");
console.log(custom_string); // "1, 2, 3, 4, 5"
