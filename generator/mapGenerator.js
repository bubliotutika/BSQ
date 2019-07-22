#!/usr/bin/env node

const process = require("process")
const fs = require("fs");
const exec = require("child_process").exec;

const Y = process.argv[2];
const X = process.argv[3];
const DENSITY = process.argv[4];

const map = new Promise((resolve, reject) => {
  exec(`perl ./map.pl ${Y} ${X} ${DENSITY}`, (err, stdout, stderr) => {
    resolve(stdout);
  });
});

const generator = async () => {
  let mapResult = "";
  let i = 0;
  const MAP = await map.then();
  
  while(MAP[i]) {
    if (MAP[i] !== "\r")
      mapResult += MAP[i];
    ++i;
  };

  fs.writeFileSync(`./maps/map_${Y}.txt`, mapResult);
  // const test = fs.readFileSync(`./maps/map_${Y}.txt`, "utf8");
  // console.log(test);
};

generator();