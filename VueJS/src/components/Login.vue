<template>
  <div class="border border-black rounded w-96 mt-10 m-auto text-center">
    <p
      class="border border-red-500 rounded-md bg-red-400 w-36 mx-auto mt-1"
      v-if="fail"
    >
      Login error
    </p>
    <from>
      <input
        type="text"
        placeholder="username"
        class="border border-black focus:border-blue-500 rounded mb-4 mt-4 w-46 h-16 text-center"
        v-model="username"
        required
      />
      <br />
      <input
        type="password"
        placeholder="password"
        class="border border-black focus:border-blue-500 rounded mb-4 mt-4 w-46 h-16 text-center"
        v-model="password"
        required
      />
      <br />
      <button
        @click="LoginClick"
        class="border-2 border-black hover:border-blue-500 rounded mb-4 mt-4 w-24 h-16"
      >
        Login
      </button>
    </from>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "Login",
  data() {
    return {
      User: [],
      username: "",
      password: "",
      fail: false,
    };
  },
  methods: {
    LoginClick() {
      axios
        .post(`http://localhost:8080/api/user/login`, {
          username: this.username,
          password: this.password,
        })
        .then((res) => {
          const userid = res.data.userid;
          this.$router.push({ path: `/todo/${userid}` });
        })
        .catch((error) => {
          console.log(error);
          this.fail = true;
        });
    },
  },
};
</script>

<style scoped></style>
