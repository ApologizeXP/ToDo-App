import { createRouter, createWebHistory } from "vue-router";
import login from "../views/login.vue";

const routes = [
  {
    path: "/",
    name: "login",
    component: login,
  },
  {
    path: "/todo/:id",
    name: "todo",
    component: () => import("../views/todo.vue"),
  },
  
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
